'use client';

import { useEffect, useState } from 'react';
import PropertyFilters from '../components/propertyFilters';
import PropertyCard from '../components/propertyCard';
import { getProperties, PropertyFilter } from '@/lib/api';

export default function HomePage() {
  const [properties, setProperties] = useState([]);
  const [loading, setLoading] = useState(false);
  const [filters, setFilters] = useState<PropertyFilter>({ page: 1, limit: 25, sortBy: 'price', sortDirection: 'asc' });
  const [totalPages, setTotalPages] = useState(1);

  const fetchData = async (customFilters?: PropertyFilter) => {
    setLoading(true);
    const current = { ...filters, ...customFilters };
    setFilters(current);

    try {
      const data = await getProperties(current);
      setProperties(data.items);
      setTotalPages(data.totalPages);
    } catch (error) {
      console.error(error);
    }
    setLoading(false);
  };

  useEffect(() => {
    fetchData();
  }, []);

  const nextPage = () => {
    if (filters.page! < totalPages) fetchData({ page: filters.page! + 1 });
  };

  const prevPage = () => {
    if (filters.page! > 1) fetchData({ page: filters.page! - 1 });
  };

  const handleSortChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const [sortBy, sortDirectionRaw] = e.target.value.split('-');
    const sortDirection = sortDirectionRaw as 'asc' | 'desc';

    fetchData({ sortBy, sortDirection, page: 1 });
  };

  return (
    <main className="max-w-6xl mx-auto p-6 space-y-6">
      <h1 className="text-3xl font-bold text-gray-800">Buscar Propiedades</h1>

      <div className="flex flex-col md:flex-row md:items-end md:justify-between gap-4">
        <PropertyFilters onSearch={(f) => fetchData({ ...f, page: 1 })} />
          <div>
            <label className="block text-sm text-gray-600 mb-1">Ordenar por</label>
            <select onChange={handleSortChange} className="border p-2 rounded w-full md:w-auto">
              <option value="price-asc">Precio ↑</option>
              <option value="price-desc">Precio ↓</option>
              <option value="name-asc">Nombre A-Z</option>
              <option value="name-desc">Nombre Z-A</option>
            </select>
          </div>
      </div>

      {loading ? (
        <p>Cargando propiedades...</p>
      ) : (
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-5 gap-6">
          {properties.map((p: any, i) => (
            <PropertyCard key={i} property={p} />
          ))}
        </div>
      )}

      <div className="flex justify-center gap-4">
        <button disabled={filters.page === 1} onClick={prevPage} className="px-4 py-2 bg-gray-200 hover:bg-gray-300 rounded disabled:opacity-50">
          Anterior
        </button>
        <span className="px-4 py-2 text-gray-700">Página {filters.page} de {totalPages}</span>
        <button disabled={filters.page === totalPages} onClick={nextPage} className="px-4 py-2 bg-gray-200 hover:bg-gray-300 rounded disabled:opacity-50">
          Siguiente
        </button>
      </div>
    </main>
  );
}