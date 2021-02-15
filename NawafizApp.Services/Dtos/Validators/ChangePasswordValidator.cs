using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Dtos.Validators
{
 public   class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            CommonRules();
        }
        private void CommonRules()
        {
            RuleFor(m => m.oldPassword).NotEmpty().WithMessage("مطلوبة");
            RuleFor(m => m.fullname).NotEmpty().WithMessage("مطلوب");
            RuleFor(m => m.UserName).NotEmpty().WithMessage("مطلوب");

            RuleFor(m => m.newPassword).NotEmpty().WithMessage("مطلوبة").Length(6,100).WithMessage("ينبغي ان يكون طول الكلمة 6 حروف على الأقل");
            RuleFor(m => m.confirmNewPassword).NotEmpty().WithMessage("مطلوبة").Equal(x => x.newPassword).WithMessage("الكلمتان غير متطابقتان");
        }
    }
}
