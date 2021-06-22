using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            //RuleSet("Name", () =>
            //{
            //    RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar adı boş olamaz");
            //    RuleFor(x => x.WriterSurName).NotEmpty().WithMessage("Yazar Soyadı boş olamaz");
            //});

            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar adı boş olamaz");
            RuleFor(x => x.WriterSurName).NotEmpty().WithMessage("Yazar Soyadı boş olamaz");

            RuleFor(x => x.WriterName).MinimumLength(3).WithMessage("Yazar adı en az 3 karakter olabilir");
            RuleFor(x => x.WriterName).MaximumLength(20).WithMessage("Yazar adı en fazla 20 karakter olabilir");

            RuleFor(x => x.WriterTitle).MinimumLength(3).WithMessage("Yazar ünvanı en az 3 karakter olabilir");
            RuleFor(x => x.WriterTitle).MaximumLength(20).WithMessage("Yazar ünvanı en fazla 20 karakter olabilir");

            RuleFor(x => x.WriterSurName).MinimumLength(2).WithMessage("Yazar soyadı en az 2 karakter olabilir");
            RuleFor(x => x.WriterSurName).MaximumLength(50).WithMessage("Yazar soyadı en fazla 50 karakter olabilir");

            RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Hakkımda boş olamaz");
            RuleFor(x => x.WriterAbout).Must(ContainsA).WithMessage("Hakkımda kısmında 'a' harfi geçmek zorundadır!");
            //hakkında da A harfi olmak zorunda
        }

        private bool ContainsA(string arg)
        {
            return arg.Contains("a");
        }
    }
}
