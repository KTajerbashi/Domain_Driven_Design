using DDD.Core.ApplicationServices.Library.Commands;
using DDD.Core.RequestResponse.Library.Commands;
using DDD.Utilities.Library;
using MiniBlog.Core.RequestResponse.Blogs.Commands.Create;
using MiniBlog.Core.RequestResponse.People.Commands.Create;

namespace MiniBlog.Core.ApplicationServices.Blogs.Commands.Create
{
    public class CreateBlogHandler : CommandHandler<CreateBlog, int>
    {
        public CreateBlogHandler(UtilitiesServices utilitiesServices) : base(utilitiesServices)
        {
        }

        public override async Task<CommandResult<int>> Handle(CreateBlog command)
        {
            return await OkAsync(0);
        }
    }
}
