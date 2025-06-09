// Application/Features/Mediator/Handlers/QuestionHandlers/UpdateFullQuestionCommandHandler.cs
using Application.Features.Mediator.Commands.QuestionCommands;
using Application.Interfaces;
using Application.Interfaces.IFileStorageService;
using Application.Interfaces.IFlashCardRepository;
using Application.Interfaces.IQuestionRepository;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Mediator.Handlers.QuestionHandlers
{
    public class UpdateFullQuestionCommandHandler : IRequestHandler<UpdateFullQuestionCommand>
    {
        private readonly IRepository<Question> _questionRepository;
        private readonly IFlashCardRepository _flashCardRepository;
        private readonly IQuestionRepository _questionImageRepository;
        private readonly IFileStorageService _fileStorageService;

        public UpdateFullQuestionCommandHandler(
            IRepository<Question> questionRepository,
            IFileStorageService fileStorageService,
            IQuestionRepository questionImageRepository,
            IFlashCardRepository flashCardRepository)
        {
            _questionRepository = questionRepository;
            _fileStorageService = fileStorageService;
            _questionImageRepository = questionImageRepository;
            _flashCardRepository = flashCardRepository;
        }

        public async Task Handle(UpdateFullQuestionCommand request, CancellationToken cancellationToken)
        {
            // 1. Fetch existing question
            var question = await _questionRepository.GetByIdAsync(request.QuestionID);
            if (question == null) throw new Exception("Soru bulunamadı");

            // 2. Partial update - only modify provided values
            if (!string.IsNullOrWhiteSpace(request.QuestionText))
                question.Text = request.QuestionText;

            if (!string.IsNullOrWhiteSpace(request.OptionA))
                question.OptionA = request.OptionA;

            if (!string.IsNullOrWhiteSpace(request.OptionB))
                question.OptionB = request.OptionB;

            if (!string.IsNullOrWhiteSpace(request.OptionC))
                question.OptionC = request.OptionC;

            if (!string.IsNullOrWhiteSpace(request.OptionD))
                question.OptionD = request.OptionD;

            if (!string.IsNullOrWhiteSpace(request.OptionE))
                question.OptionE = request.OptionE;



                question.Answer = request.Answer;

            await _questionRepository.UpdateAsync(question);

            // 3. Handle image updates (only process if file is provided)
            await ProcessImageUpdate(request.QuestionImage, QuestionImageType.Question, question.QuestionID, cancellationToken);
            await ProcessImageUpdate(request.OptionAImage, QuestionImageType.OptionA, question.QuestionID, cancellationToken);
            await ProcessImageUpdate(request.OptionBImage, QuestionImageType.OptionB, question.QuestionID, cancellationToken);
            await ProcessImageUpdate(request.OptionCImage, QuestionImageType.OptionC, question.QuestionID, cancellationToken);
            await ProcessImageUpdate(request.OptionDImage, QuestionImageType.OptionD, question.QuestionID, cancellationToken);
            await ProcessImageUpdate(request.OptionEImage, QuestionImageType.OptionE, question.QuestionID, cancellationToken);

            // 4. Handle flashcard operations
            await HandleFlashCardUpdate(request, question.QuestionID);

            return ;
        }

        private async Task ProcessImageUpdate(IFormFile? imageFile, QuestionImageType imageType, int questionId, CancellationToken cancellationToken)
        {
            // Skip if no file provided
            if (imageFile == null || imageFile.Length == 0) return;

            var existingImage = await _questionImageRepository.GetByQuestionIdAndTypeAsync(questionId, imageType);

            // Delete old file if exists
            if (existingImage != null)
            {
                await _fileStorageService.DeleteFileAsync(existingImage.ImageUrl);
            }

            // Upload new file
            var newImagePath = await _fileStorageService.SaveFileAsync(imageFile, "question-images", cancellationToken);

            if (existingImage != null)
            {
                // Update existing image record
                existingImage.ImageUrl = newImagePath;
                await _questionImageRepository.UpdateImgAsync(existingImage);
            }
            else
            {
                // Create new image record
                await _questionImageRepository.AddImgAsync(new QuestionImage
                {
                    QuestionID = questionId,
                    Type = imageType,
                    ImageUrl = newImagePath
                });
            }
        }

        private async Task HandleFlashCardUpdate(UpdateFullQuestionCommand request, int questionId)
        {
            var existingFlashCards = await _flashCardRepository.GetFlashCardsByQuestionIdAsync(questionId);
            var existingFlashCard = existingFlashCards.FirstOrDefault();

            // Handle flashcard removal
            if (request.RemoveFlashCard)
            {
                if (existingFlashCard != null)
                {
                    await _flashCardRepository.DeleteAsync(existingFlashCard);
                }
                return;
            }

            // Skip update if no flashcard data provided
            if (string.IsNullOrWhiteSpace(request.FlashCardFront) &&
                string.IsNullOrWhiteSpace(request.FlashCardBack))
            {
                return;
            }

            if (existingFlashCard != null)
            {
                // Partial update - only modify provided values
                if (!string.IsNullOrWhiteSpace(request.FlashCardFront))
                    existingFlashCard.Front = request.FlashCardFront;

                if (!string.IsNullOrWhiteSpace(request.FlashCardBack))
                    existingFlashCard.Back = request.FlashCardBack;

                await _flashCardRepository.UpdateAsync(existingFlashCard);
            }
            else
            {
                // Create new flashcard with provided values
                await _flashCardRepository.CreateAsync(new FlashCard
                {
                    Front = request.FlashCardFront ?? "",
                    Back = request.FlashCardBack ?? "",
                    QuestionID = questionId
                });
            }
        }
    }
}