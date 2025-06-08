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
            if (question == null) throw new Exception("Question not found");

            // 2. Update question properties
            question.Text = request.QuestionText;
            question.OptionA = request.OptionA;
            question.OptionB = request.OptionB;
            question.OptionC = request.OptionC;
            question.OptionD = request.OptionD;
            question.OptionE = request.OptionE;
            question.TestID = request.TestId;
            question.Answer = request.Answer;

            await _questionRepository.UpdateAsync(question);

            // 3. Handle image updates
            await UpdateQuestionImage(request.QuestionImage, QuestionImageType.Question, question.QuestionID, cancellationToken);
            await UpdateQuestionImage(request.OptionAImage, QuestionImageType.OptionA, question.QuestionID, cancellationToken);
            await UpdateQuestionImage(request.OptionBImage, QuestionImageType.OptionB, question.QuestionID, cancellationToken);
            await UpdateQuestionImage(request.OptionCImage, QuestionImageType.OptionC, question.QuestionID, cancellationToken);
            await UpdateQuestionImage(request.OptionDImage, QuestionImageType.OptionD, question.QuestionID, cancellationToken);
            await UpdateQuestionImage(request.OptionEImage, QuestionImageType.OptionE, question.QuestionID, cancellationToken);

            // 4. Handle flashcard operations
            await HandleFlashCardUpdate(request, question.QuestionID);

            return ;
        }

        private async Task UpdateQuestionImage(IFormFile? imageFile, QuestionImageType imageType, int questionId, CancellationToken cancellationToken)
        {
            if (imageFile == null) return;

            // Get existing image
            var existingImage = await _questionImageRepository.GetByQuestionIdAndTypeAsync(questionId, imageType);

            // Delete old file if exists
            if (existingImage != null)
            {
                await _fileStorageService.DeleteFileAsync(existingImage.ImageUrl);
            }

            // Upload new file
            var newImagePath = await _fileStorageService.SaveFileAsync(imageFile, "question-images", cancellationToken);

            // Create or update image record
            if (existingImage != null)
            {
                existingImage.ImageUrl = newImagePath;
                await _questionImageRepository.UpdateImgAsync(existingImage);
            }
            else
            {
                var newImage = new QuestionImage
                {
                    QuestionID = questionId,
                    Type = imageType,
                    ImageUrl = newImagePath
                };
                await _questionImageRepository.AddImgAsync(newImage);
            }
        }

        private async Task HandleFlashCardUpdate(UpdateFullQuestionCommand request, int questionId)
        {
            // Get existing flashcard by question ID
            var existingFlashCards = await _flashCardRepository.GetFlashCardsByQuestionIdAsync(questionId);
            var existingFlashCard = existingFlashCards.FirstOrDefault();

            // Case 1: Remove flashcard if requested or both fields empty
            if (request.RemoveFlashCard ||
               (string.IsNullOrWhiteSpace(request.FlashCardFront) &&
                string.IsNullOrWhiteSpace(request.FlashCardBack)))
            {
                if (existingFlashCard != null)
                {
                    // Implement flashcard deletion in your repository
                    await _flashCardRepository.DeleteAsync(existingFlashCard);
                }
                return;
            }

            // Case 2: Update or create flashcard
            if (existingFlashCard != null)
            {
                existingFlashCard.Front = request.FlashCardFront ?? "";
                existingFlashCard.Back = request.FlashCardBack ?? "";
                await _flashCardRepository.UpdateAsync(existingFlashCard);
            }
            else
            {
                var newFlashCard = new FlashCard
                {
                    Front = request.FlashCardFront ?? "",
                    Back = request.FlashCardBack ?? "",
                    QuestionID = questionId
                };
                await _flashCardRepository.CreateAsync(newFlashCard);
            }
        }
    }
}