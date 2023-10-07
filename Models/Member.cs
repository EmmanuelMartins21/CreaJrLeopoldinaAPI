using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;

namespace CreaJrLeopoldinaAPI.Models
{
    public class Member
    {

        #region Propriedades

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Key]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O cargo é obrigatório.")]
        public string Position { get; set; } = string.Empty;


        [Required(ErrorMessage = "A coordenação é obrigatória.")]
        public string Coordination { get; set; } = string.Empty;

        [Required]
        public bool Enable { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birth { get; set; }

        [Required(ErrorMessage = "A data de inicio é obrigatória.")]
        [Display(Name = "Data de inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [CustomValidation(typeof(Member), "ValidateStartingDate")]
        public DateTime StartingDate { get; set; }

        #endregion

        #region Validations
        public static ValidationResult ValidateBirthDate(DateTime dataNascimento, ValidationContext context)
        {
            if (dataNascimento <= DateTime.Now.Date)
            {
                return new ValidationResult("A data de nascimento deve ser anterior à data atual.");
            }
            return ValidationResult.Success;
        }

        public static ValidationResult ValidateStartingDate(DateTime startingDate, ValidationContext context)
        {
            if (startingDate > DateTime.Now.Date)
            {
                return new ValidationResult("A data de inicio deve ser anterior ou igual à data atual.");
            }
            return ValidationResult.Success;
        }

        #endregion

        public override string ToString()
        {
            return "Não existe aniversariantes nesse mês.";
        }
    }
}
