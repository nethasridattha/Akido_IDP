using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class CategoryResponse: BaseResponse<Category>
    {
        public CategoryResponse(Category category) : base(category) { }

        public CategoryResponse(string message) : base(message) { }

        public CategoryResponse(Category category, string message) : base(category, message) { }
    }
}
