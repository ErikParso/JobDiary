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
    public class DietPaymentItemController : TableController<DietPaymentItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MyJobDiaryContext context = new MyJobDiaryContext();
            DomainManager = new EntityDomainManager<DietPaymentItem>(context, Request);
        }

        // GET tables/TodoItem
        public IQueryable<DietPaymentItem> GetAllTodoItems()
        {
            string userId = GetUserId(User);
            return Query().Where(s => s.UserId == userId);
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<DietPaymentItem> GetTodoItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<DietPaymentItem> PatchTodoItem(string id, Delta<DietPaymentItem> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/TodoItem
        public async Task<IHttpActionResult> PostTodoItem(DietPaymentItem item)
        {
            item.UserId = GetUserId(User);
            DietPaymentItem current = await InsertAsync(item);
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