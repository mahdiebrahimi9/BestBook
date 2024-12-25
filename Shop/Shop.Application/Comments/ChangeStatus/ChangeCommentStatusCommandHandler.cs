﻿using Common.Application;
using Shop.Domain.CommentAgg.Repository;

namespace Shop.Application.Comments.ChangeStatus
{
    public class ChangeCommentStatusCommandHandler : IBaseCommandHandler<ChangeCommentStatusCommand>
    {
        private readonly ICommentRepository _repository;

        public ChangeCommentStatusCommandHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(ChangeCommentStatusCommand request, CancellationToken cancellationToken)
        {
            var comment = await _repository.GetTracking(request.id);
            if (comment == null)
                return OperationResult.NotFound();

            comment.ChangeStatus(request.status);
            await _repository.Save();

            return OperationResult.Success();
        }
    }

}
