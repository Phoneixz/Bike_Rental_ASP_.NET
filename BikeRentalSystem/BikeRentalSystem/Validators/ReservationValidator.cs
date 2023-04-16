using BikeRentalSystem.Models;
using FluentValidation;

namespace BikeRentalSystem.Validators
{
    public class ReservationValidator : AbstractValidator<Reservation>
    {
        public ReservationValidator()
        {
            RuleFor(r => r.PickUpDate)
                .NotNull().WithMessage("Pickup date is required.")
                .LessThan(r => r.ReturnDate).WithMessage("Pickup date should be earlier than the return date");
            RuleFor(r => r.ReturnDate)
                .NotNull().WithMessage("Return date is required.")
                .GreaterThan(r => r.PickUpDate).WithMessage("Return date should be later than the pickup date");
        }
    }
}
