using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Vendr.Contrib.ProductReviews.Web.Dtos
{
    [DataContract(Name = "review", Namespace = "")]
    public class ReviewSaveDto : ReviewDto, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return Enumerable.Empty<ValidationResult>();
        }
    }
}