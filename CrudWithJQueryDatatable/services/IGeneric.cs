﻿using CrudWithJQueryDatatable.Models.DataTable;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudWithJQueryDatatable.services
{
    public interface IGeneric
    {
        Task<List<UserPartial>> GetUserAsync(ListingRequest request);
    }
}