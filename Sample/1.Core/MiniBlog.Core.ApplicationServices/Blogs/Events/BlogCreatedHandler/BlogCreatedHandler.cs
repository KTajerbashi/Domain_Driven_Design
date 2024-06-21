using DDD.Core.ApplicationServices.Library.Commands;
using DDD.Core.RequestResponse.Library.Commands;
using DDD.Utilities.Library;
using MiniBlog.Core.RequestResponse.Blogs.Commands.Create;
using MiniBlog.Core.RequestResponse.People.Commands.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBlog.Core.ApplicationServices.Blogs.Events.BlogCreatedHandler
{
    public class BlogCreatedHandler : CommandHandler<CreateBlog, int>
    {
        public BlogCreatedHandler(UtilitiesServices utilitiesServices) : base(utilitiesServices)
        {
        }

        public override async Task<CommandResult<int>> Handle(CreateBlog command)
        {
            return await OkAsync(0);
        }
    }
}
