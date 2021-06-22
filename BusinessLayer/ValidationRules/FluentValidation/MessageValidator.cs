using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.MessageContent).NotEmpty().WithMessage("Mesaj boş olamaz");
            RuleFor(x => x.MessageContent).MinimumLength(5).WithMessage("Mesaj metni en az 5 karakterli olmalıdır");

            RuleFor(x => x.ReceiverMail).NotEmpty().WithMessage("Alıcı adresi boş olamaz");
            RuleFor(x => x.ReceiverMail).EmailAddress().WithMessage("Gerçek mail adresi olmalı!");

            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu boş olamaz");
            RuleFor(x => x.Subject).MinimumLength(5).WithMessage("Konu metni en az 5 karakterli olmalıdır");
            RuleFor(x => x.Subject).MaximumLength(100).WithMessage("Konu metni en fazla 100 karakterli olmalıdır");
        }
    }
}
