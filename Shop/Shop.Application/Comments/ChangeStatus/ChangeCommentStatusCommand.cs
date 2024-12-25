﻿using Common.Application;
using FluentValidation;
using Shop.Domain.CommentAgg.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Comments.ChangeStatus
{
    public record ChangeCommentStatusCommand(long id, CommentStatus status) : IBaseCommand;

}
