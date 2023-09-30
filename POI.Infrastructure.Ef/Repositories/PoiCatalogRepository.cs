using POI.Domain.Entities;
using POI.Domain.Repositories;
using POI.Infrastructure.Ef.Base;
using POI.Infrastructure.Ef.Context;

namespace POI.Infrastructure.Ef.Repositories;

public class PoiCatalogRepository : BaseRepository<PoiCatalog>, IPoiCatalogRepository
{
    #region Constructors

    public PoiCatalogRepository(PoiDbContext context)
        : base(context)
    {

    }

    #endregion

    #region Methods

    protected IQueryable<PoiCatalog> Queryable()
    {
        return _entitySet.AsQueryable();
    }

    #endregion
}