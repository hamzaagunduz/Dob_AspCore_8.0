﻿using Application.Features.Mediator.Results.PaymentResults;
using Application.Interfaces.IPaymentService;
using Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Persistence.Hubs;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Azure.Core;
using Microsoft.Extensions.Options;
using Persistence.Providers;

namespace Persistence.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IHubContext<PayHub> _hubContext;
        private readonly IyzipaySettingsProvider _settingsProvider;
        private Iyzipay.Options _options;
        private string? _callbackUrl;


        public PaymentService(IHubContext<PayHub> hubContext, IyzipaySettingsProvider settingsProvider)
        {
            _hubContext = hubContext;
            _settingsProvider = settingsProvider;
        }

        private async Task<Iyzipay.Options> GetOptionsAsync()
        {
            if (_options != null && _callbackUrl != null)
                return _options;

            var settings = await _settingsProvider.GetSettingsAsync();

            _options = new Iyzipay.Options
            {
                ApiKey = settings.ApiKey,
                SecretKey = settings.SecretKey,
                BaseUrl = settings.BaseUrl
            };

            _callbackUrl = settings.CallbackUrl;
            return _options;
        }

        public async Task<CreatePaymentResult> CreatePaymentAsync(
            string cardHolderName,
            string cardNumber,
            string expireMonth,
            string expireYear,
            string cvc)
        {

            var options = await GetOptionsAsync();

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = Guid.NewGuid().ToString();
            request.Price = "1";
            request.PaidPrice = "1.2";
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
            request.CallbackUrl = _callbackUrl;

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = cardHolderName;
            paymentCard.CardNumber = cardNumber;
            paymentCard.ExpireMonth = expireMonth;
            paymentCard.ExpireYear = expireYear;
            paymentCard.Cvc = cvc;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;
            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = "John";
            buyer.Surname = "Doe";
            buyer.GsmNumber = "+905350000000";
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();

            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = "BI101";
            firstBasketItem.Name = "Binocular";
            firstBasketItem.Category1 = "Collectibles";
            firstBasketItem.Category2 = "Accessories";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = "0.3";
            basketItems.Add(firstBasketItem);

            BasketItem secondBasketItem = new BasketItem();
            secondBasketItem.Id = "BI102";
            secondBasketItem.Name = "Game code";
            secondBasketItem.Category1 = "Game";
            secondBasketItem.Category2 = "Online Game Items";
            secondBasketItem.ItemType = BasketItemType.VIRTUAL.ToString();
            secondBasketItem.Price = "0.5";
            basketItems.Add(secondBasketItem);

            BasketItem thirdBasketItem = new BasketItem();
            thirdBasketItem.Id = "BI103";
            thirdBasketItem.Name = "Usb";
            thirdBasketItem.Category1 = "Electronics";
            thirdBasketItem.Category2 = "Usb / Cable";
            thirdBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            thirdBasketItem.Price = "0.2";
            basketItems.Add(thirdBasketItem);

            request.BasketItems = basketItems;

            ThreedsInitialize threedsInitialize = await ThreedsInitialize.Create(request, options);

            return new CreatePaymentResult(
                HtmlContent: threedsInitialize.HtmlContent,
                ConversationId: request.ConversationId,
                errorMessage:threedsInitialize.ErrorMessage
            );

        }

        public async Task HandleCallbackAsync(PaymentCallback callbackData)
        {
            CreateThreedsPaymentRequest request = new CreateThreedsPaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = callbackData.ConversationId;
            request.PaymentId = callbackData.PaymentId;
            request.ConversationData = callbackData.ConversationData;
            var options = await GetOptionsAsync();

            ThreedsPayment threedsPayment = await ThreedsPayment.Create(request, options);

            var responseCheck = new
            {
                status = threedsPayment.Status, // "success" veya "failure"
                paymentId = threedsPayment.PaymentId,
                conversationData = new
                {
                    conversationId = threedsPayment.ConversationId,
                    mdStatus = threedsPayment.MdStatus
                }
            };

            // Client bağlantısını kontrol et
            if (PayHub.TransactionConnections.TryGetValue(threedsPayment.ConversationId, out var connectionId))
            {
                await _hubContext.Clients
                    .Client(connectionId)
                    .SendAsync("Receive", responseCheck);
            }

            if (threedsPayment.Status != "success")
            {
                return;
            }

        }

    }
}
