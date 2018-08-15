using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using MyJobDiaryService.DataObjects;
using MyJobDiaryService.Models;

namespace MyJobDiaryService.Controllers
{
    [Authorize]
    public class DietRecordController : TableController<DietRecord>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MyJobDiaryContext context = new MyJobDiaryContext();
            DomainManager = new EntityDomainManager<DietRecord>(context, Request);
        }

        // GET tables/TodoItem
        public IQueryable<DietRecord> GetAllTodoItems()
        {
            string userId = GetUserId(User);
            return Query().Where(d => d.UserId == userId);
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<DietRecord> GetTodoItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<DietRecord> PatchTodoItem(string id, Delta<DietRecord> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/TodoItem
        public async Task<IHttpActionResult> PostTodoItem(DietRecord item)
        {
            item.UserId = GetUserId(User);
            DietRecord current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTodoItem(string id)
        {
            return DeleteAsync(id);
        }

        private string GetUserId(IPrincipal user)
        {
            ClaimsPrincipal claimsUser = (ClaimsPrincipal)user;
            string sid = claimsUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            return sid;
        }
    }
}