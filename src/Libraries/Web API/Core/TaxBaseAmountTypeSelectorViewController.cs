using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.EntityParser;
using Newtonsoft.Json;
using PetaPoco;

namespace MixERP.Net.Api.Core
{
    /// <summary>
    ///     Provides a direct HTTP access to perform various tasks such as adding, editing, and removing Tax Base Amount Type Selector Views.
    /// </summary>
    [RoutePrefix("api/v1.5/core/tax-base-amount-type-selector-view")]
    public class TaxBaseAmountTypeSelectorViewController : ApiController
    {
        /// <summary>
        ///     The TaxBaseAmountTypeSelectorView data context.
        /// </summary>
        private readonly MixERP.Net.Schemas.Core.Data.TaxBaseAmountTypeSelectorView TaxBaseAmountTypeSelectorViewContext;

        public TaxBaseAmountTypeSelectorViewController()
        {
            this.LoginId = AppUsers.GetCurrent().View.LoginId.ToLong();
            this.UserId = AppUsers.GetCurrent().View.UserId.ToInt();
            this.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
            this.Catalog = AppUsers.GetCurrentUserDB();

            this.TaxBaseAmountTypeSelectorViewContext = new MixERP.Net.Schemas.Core.Data.TaxBaseAmountTypeSelectorView
            {
                Catalog = this.Catalog,
                LoginId = this.LoginId
            };
        }

        public long LoginId { get; }
        public int UserId { get; private set; }
        public int OfficeId { get; private set; }
        public string Catalog { get; }

        /// <summary>
        ///     Counts the number of tax base amount type selector views.
        /// </summary>
        /// <returns>Returns the count of the tax base amount type selector views.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("count")]
        [Route("~/api/core/tax-base-amount-type-selector-view/count")]
        public long Count()
        {
            try
            {
                return this.TaxBaseAmountTypeSelectorViewContext.Count();
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }


        /// <summary>
        ///     Creates a paginated collection containing 25 tax base amount type selector views on each page, sorted by the property .
        /// </summary>
        /// <returns>Returns the first page from the collection.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("")]
        [Route("~/api/core/tax-base-amount-type-selector-view")]
        public IEnumerable<MixERP.Net.Entities.Core.TaxBaseAmountTypeSelectorView> GetPagedResult()
        {
            try
            {
                return this.TaxBaseAmountTypeSelectorViewContext.GetPagedResult();
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Creates a paginated collection containing 25 tax base amount type selector views on each page, sorted by the property .
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the resultset.</param>
        /// <returns>Returns the requested page from the collection.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("page/{pageNumber}")]
        [Route("~/api/core/tax-base-amount-type-selector-view/page/{pageNumber}")]
        public IEnumerable<MixERP.Net.Entities.Core.TaxBaseAmountTypeSelectorView> GetPagedResult(long pageNumber)
        {
            try
            {
                return this.TaxBaseAmountTypeSelectorViewContext.GetPagedResult(pageNumber);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Creates a filtered and paginated collection containing 25 tax base amount type selector views on each page, sorted by the property .
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the resultset.</param>
        /// <param name="filters">The list of filter conditions.</param>
        /// <returns>Returns the requested page from the collection using the supplied filters.</returns>
        [AcceptVerbs("POST")]
        [Route("get-where/{pageNumber}")]
        [Route("~/api/core/tax-base-amount-type-selector-view/get-where/{pageNumber}")]
        public IEnumerable<MixERP.Net.Entities.Core.TaxBaseAmountTypeSelectorView> GetWhere(long pageNumber, [FromBody]dynamic filters)
        {
            try
            {
                List<EntityParser.Filter> f = JsonConvert.DeserializeObject<List<EntityParser.Filter>>(filters);
                return this.TaxBaseAmountTypeSelectorViewContext.GetWhere(pageNumber, f);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

    }
}