/********************************************************************************
Copyright (C) MixERP Inc. (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, version 2 of the License.


MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MixERP.Net.DbFactory;
using MixERP.Net.EntityParser;
using MixERP.Net.Framework;
using Npgsql;
using PetaPoco;
using Serilog;

namespace MixERP.Net.Schemas.Policy.Data
{
    /// <summary>
    /// Provides simplified data access features to perform SCRUD operation on the database table "policy.api_access_policy".
    /// </summary>
    public class ApiAccessPolicy : DbAccess
    {
        /// <summary>
        /// The schema of this table. Returns literal "policy".
        /// </summary>
	    public override string ObjectNamespace => "policy";

        /// <summary>
        /// The schema unqualified name of this table. Returns literal "api_access_policy".
        /// </summary>
	    public override string ObjectName => "api_access_policy";

        /// <summary>
        /// Login id of application user accessing this table.
        /// </summary>
		public long LoginId { get; set; }

        /// <summary>
        /// The name of the database on which queries are being executed to.
        /// </summary>
        public string Catalog { get; set; }

		/// <summary>
		/// Performs SQL count on the table "policy.api_access_policy".
		/// </summary>
		/// <returns>Returns the number of rows of the table "policy.api_access_policy".</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public long Count()
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return 0;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to count entity \"ApiAccessPolicy\" was denied to the user with Login ID {LoginId}", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			const string sql = "SELECT COUNT(*) FROM policy.api_access_policy;";
			return Factory.Scalar<long>(this.Catalog, sql);
		}

		/// <summary>
		/// Executes a select query on the table "policy.api_access_policy" with a where filter on the column "api_access_policy_id" to return a single instance of the "ApiAccessPolicy" class. 
		/// </summary>
		/// <param name="apiAccessPolicyId">The column "api_access_policy_id" parameter used on where filter.</param>
		/// <returns>Returns a non-live, non-mapped instance of "ApiAccessPolicy" class mapped to the database row.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public MixERP.Net.Entities.Policy.ApiAccessPolicy Get(long apiAccessPolicyId)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return null;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to the get entity \"ApiAccessPolicy\" filtered by \"ApiAccessPolicyId\" with value {ApiAccessPolicyId} was denied to the user with Login ID {LoginId}", apiAccessPolicyId, this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			const string sql = "SELECT * FROM policy.api_access_policy WHERE api_access_policy_id=@0;";
			return Factory.Get<MixERP.Net.Entities.Policy.ApiAccessPolicy>(this.Catalog, sql, apiAccessPolicyId).FirstOrDefault();
		}

        /// <summary>
        /// Custom fields are user defined form elements for policy.api_access_policy.
        /// </summary>
        /// <returns>Returns an enumerable custom field collection for the table policy.api_access_policy</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<PetaPoco.CustomField> GetCustomFields(string resourceId)
        {
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return null;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to get custom fields for entity \"ApiAccessPolicy\" was denied to the user with Login ID {LoginId}", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            string sql;
			if (string.IsNullOrWhiteSpace(resourceId))
            {
				sql = "SELECT * FROM core.custom_field_definition_view WHERE table_name='policy.api_access_policy' ORDER BY field_order;";
				return Factory.Get<PetaPoco.CustomField>(this.Catalog, sql);
            }

            sql = "SELECT * from core.get_custom_field_definition('policy.api_access_policy'::text, @0::text) ORDER BY field_order;";
			return Factory.Get<PetaPoco.CustomField>(this.Catalog, sql, resourceId);
        }

        /// <summary>
        /// Displayfields provide a minimal name/value context for data binding the row collection of policy.api_access_policy.
        /// </summary>
        /// <returns>Returns an enumerable name and value collection for the table policy.api_access_policy</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public IEnumerable<DisplayField> GetDisplayFields()
		{
			List<DisplayField> displayFields = new List<DisplayField>();

			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return displayFields;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to get display field for entity \"ApiAccessPolicy\" was denied to the user with Login ID {LoginId}", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			const string sql = "SELECT api_access_policy_id AS key, poco_type_name as value FROM policy.api_access_policy;";
			using (NpgsqlCommand command = new NpgsqlCommand(sql))
			{
				using (DataTable table = DbOperation.GetDataTable(this.Catalog, command))
				{
					if (table?.Rows == null || table.Rows.Count == 0)
					{
						return displayFields;
					}

					foreach (DataRow row in table.Rows)
					{
						if (row != null)
						{
							DisplayField displayField = new DisplayField
							{
								Key = row["key"].ToString(),
								Value = row["value"].ToString()
							};

							displayFields.Add(displayField);
						}
					}
				}
			}

			return displayFields;
		}

		/// <summary>
		/// Inserts or updates the instance of ApiAccessPolicy class on the database table "policy.api_access_policy".
		/// </summary>
		/// <param name="apiAccessPolicy">The instance of "ApiAccessPolicy" class to insert or update.</param>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public void AddOrEdit(MixERP.Net.Entities.Policy.ApiAccessPolicy apiAccessPolicy)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return;
			}

			if(apiAccessPolicy.ApiAccessPolicyId > 0){
				this.Update(apiAccessPolicy, apiAccessPolicy.ApiAccessPolicyId);
				return;
			}
	
			this.Add(apiAccessPolicy);
		}

		/// <summary>
		/// Inserts the instance of ApiAccessPolicy class on the database table "policy.api_access_policy".
		/// </summary>
		/// <param name="apiAccessPolicy">The instance of "ApiAccessPolicy" class to insert.</param>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public void Add(MixERP.Net.Entities.Policy.ApiAccessPolicy apiAccessPolicy)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Create, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to add entity \"ApiAccessPolicy\" was denied to the user with Login ID {LoginId}. {ApiAccessPolicy}", this.LoginId, apiAccessPolicy);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			Factory.Insert(this.Catalog, apiAccessPolicy);
		}

		/// <summary>
		/// Updates the row of the table "policy.api_access_policy" with an instance of "ApiAccessPolicy" class against the primary key value.
		/// </summary>
		/// <param name="apiAccessPolicy">The instance of "ApiAccessPolicy" class to update.</param>
		/// <param name="apiAccessPolicyId">The value of the column "api_access_policy_id" which will be updated.</param>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public void Update(MixERP.Net.Entities.Policy.ApiAccessPolicy apiAccessPolicy, long apiAccessPolicyId)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Edit, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to edit entity \"ApiAccessPolicy\" with Primary Key {PrimaryKey} was denied to the user with Login ID {LoginId}. {ApiAccessPolicy}", apiAccessPolicyId, this.LoginId, apiAccessPolicy);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			Factory.Update(this.Catalog, apiAccessPolicy, apiAccessPolicyId);
		}

		/// <summary>
		/// Deletes the row of the table "policy.api_access_policy" against the primary key value.
		/// </summary>
		/// <param name="apiAccessPolicyId">The value of the column "api_access_policy_id" which will be deleted.</param>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public void Delete(long apiAccessPolicyId)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Delete, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to delete entity \"ApiAccessPolicy\" with Primary Key {PrimaryKey} was denied to the user with Login ID {LoginId}.", apiAccessPolicyId, this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			const string sql = "DELETE FROM policy.api_access_policy WHERE api_access_policy_id=@0;";
			Factory.NonQuery(this.Catalog, sql, apiAccessPolicyId);
		}

		/// <summary>
		/// Performs a select statement on table "policy.api_access_policy" producing a paged result of 25.
		/// </summary>
		/// <returns>Returns the first page of collection of "ApiAccessPolicy" class.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public IEnumerable<MixERP.Net.Entities.Policy.ApiAccessPolicy> GetPagedResult()
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return null;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to the first page of the entity \"ApiAccessPolicy\" was denied to the user with Login ID {LoginId}.", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			const string sql = "SELECT * FROM policy.api_access_policy ORDER BY api_access_policy_id LIMIT 25 OFFSET 0;";
			return Factory.Get<MixERP.Net.Entities.Policy.ApiAccessPolicy>(this.Catalog, sql);
		}

		/// <summary>
		/// Performs a select statement on table "policy.api_access_policy" producing a paged result of 25.
		/// </summary>
		/// <param name="pageNumber">Enter the page number to produce the paged result.</param>
		/// <returns>Returns collection of "ApiAccessPolicy" class.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public IEnumerable<MixERP.Net.Entities.Policy.ApiAccessPolicy> GetPagedResult(long pageNumber)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return null;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to Page #{Page} of the entity \"ApiAccessPolicy\" was denied to the user with Login ID {LoginId}.", pageNumber, this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			long offset = (pageNumber -1) * 25;
			const string sql = "SELECT * FROM policy.api_access_policy ORDER BY api_access_policy_id LIMIT 25 OFFSET @0;";
				
			return Factory.Get<MixERP.Net.Entities.Policy.ApiAccessPolicy>(this.Catalog, sql, offset);
		}

        /// <summary>
		/// Performs a filtered select statement on table "policy.api_access_policy" producing a paged result of 25.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the paged result.</param>
        /// <param name="filters">The list of filter conditions.</param>
		/// <returns>Returns collection of "ApiAccessPolicy" class.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<MixERP.Net.Entities.Policy.ApiAccessPolicy> GetWhere(long pageNumber, List<EntityParser.Filter> filters)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to Page #{Page} of the filtered entity \"ApiAccessPolicy\" was denied to the user with Login ID {LoginId}. Filters: {Filters}.", pageNumber, this.LoginId, filters);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            long offset = (pageNumber - 1) * 25;
            Sql sql = Sql.Builder.Append("SELECT * FROM policy.api_access_policy WHERE 1 = 1");

            MixERP.Net.EntityParser.Data.Service.AddFilters(ref sql, new MixERP.Net.Entities.Policy.ApiAccessPolicy(), filters);

            sql.OrderBy("api_access_policy_id");
            sql.Append("LIMIT @0", 25);
            sql.Append("OFFSET @0", offset);

            return Factory.Get<MixERP.Net.Entities.Policy.ApiAccessPolicy>(this.Catalog, sql);
        }

        public IEnumerable<MixERP.Net.Entities.Policy.ApiAccessPolicy> Get(long[] apiAccessPolicyIds)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to entity \"ApiAccessPolicy\" was denied to the user with Login ID {LoginId}. apiAccessPolicyIds: {apiAccessPolicyIds}.", this.LoginId, apiAccessPolicyIds);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

			const string sql = "SELECT * FROM policy.api_access_policy WHERE api_access_policy_id IN (@0);";

            return Factory.Get<MixERP.Net.Entities.Policy.ApiAccessPolicy>(this.Catalog, sql, apiAccessPolicyIds);
        }
	}
}