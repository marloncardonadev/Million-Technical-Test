'use client';

import { useEffect, useState, useCallback, useRef } from 'react';
import PropertyFilters from '../components/propertyFilters';
import PropertyCard from '../components/propertyCard';
import PropertyCardSkeleton from '../components/propertyCardSkeleton';
import { getProperties } from '@/lib/api';
import { Property } from '@/models/property';
import PropertyDetail from '@/components/propertyDetail';
import { PropertyFilter } from '@/models/propertyFilter';

export default function HomePage() {
  const [properties, setProperties] = useState<Property[]>([]);
  const [loading, setLoading] = useState(false);
  const [filters, setFilters] = useState<PropertyFilter>({
    page: 1,
    limit: 25,
    sortBy: 'Price',
    sortDirection: 'asc'
  });
  const [totalPages, setTotalPages] = useState(1);
  const [selectedId, setSelectedId] = useState<string | null>(null);
  
  const isFetching = useRef(false);

  const fetchData = useCallback(async (customFilters?: Partial<PropertyFilter>) => {
    if (isFetching.current) return;
    
    isFetching.current = true;
    setLoading(true);
    
    const newFilters = { ...filters, ...customFilters };
    
    try {
      const data = await getProperties(newFilters);
      setProperties(data.items);
      setTotalPages(data.totalPages);
      
      if (customFilters?.page !== undefined) {
        setFilters(prev => ({ ...prev, page: customFilters.page! }));
      }
    } catch (error) {
      console.error('Error fetching properties:', error);
    } finally {
      setLoading(false);
      isFetching.current = false;
    }
  }, [filters]);

  useEffect(() => {
    const abortController = new AbortController();
    let mounted = true;

    const fetchInitialData = async () => {
      if (!mounted) return;
      
      setLoading(true);
      try {
        const data = await getProperties(filters);
        if (mounted) {
          setProperties(data.items);
          setTotalPages(data.totalPages);
        }
      } catch (error) {
        if (mounted) console.error(error);
      } finally {
        if (mounted) setLoading(false);
      }
    };

    fetchInitialData();

    return () => {
      mounted = false;
      abortController.abort();
    };
  }, []);

  const nextPage = useCallback((e?: React.MouseEvent) => {
    if (e) {
      e.preventDefault();
      e.stopPropagation();
    }
    
    if (filters.page! < totalPages && !isFetching.current) {
      fetchData({ page: filters.page! + 1 });
    }
  }, [filters.page, totalPages, fetchData]);

  const prevPage = useCallback((e?: React.MouseEvent) => {
    if (e) {
      e.preventDefault();
      e.stopPropagation();
    }
    
    if (filters.page! > 1 && !isFetching.current) {
      fetchData({ page: filters.page! - 1 });
    }
  }, [filters.page, fetchData]);

  useEffect(() => {
    console.log('Página actual:', filters.page);
  }, [filters.page]);

  return (
    <main className="max-w-6xl mx-auto p-6 space-y-6">
      <form className="bg-white p-4 rounded-xl shadow-md space-y-4">
        <h1 className="text-3xl font-bold text-gray-800">TECHNICAL TEST MILLON</h1>
      </form>
      
      <PropertyFilters
        onSearch={(f) => fetchData({ ...f, page: 1 })}
        onSortChange={(sortBy, sortDirection) =>
          fetchData({ sortBy, sortDirection, page: 1 })
        }
      />

      {loading ? (
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-5 gap-6 items-stretch">
          {Array.from({ length: 5 }).map((_, i) => (
            <PropertyCardSkeleton key={i} />
          ))}
          <strong>Cargando propiedades...</strong>
        </div>
      ) : (
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-5 gap-6 items-stretch">
          {properties.map((p, i) => (
            <PropertyCard
              key={p.id || i}
              property={p}
              onClick={() => setSelectedId(p.id)}
            />
          ))}
        </div>
      )}

      {selectedId && (
        <div
          className="fixed inset-0 flex items-center justify-center p-6 z-50"
          onClick={() => setSelectedId(null)}
        >
          <div
            className="bg-white rounded-2xl shadow-2xl max-w-4xl w-full p-6 relative overflow-y-auto max-h-[90vh]"
            onClick={(e) => e.stopPropagation()}
          >
            <button
              onClick={() => setSelectedId(null)}
              className="absolute top-4 right-4 w-10 h-10 flex items-center justify-center rounded-full bg-gray-100 hover:bg-gray-200 transition-all duration-200 shadow-md hover:shadow-md"
            >
              <svg
                xmlns="http://www.w3.org/2000/svg"
                className="w-5 h-5 text-gray-600"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
                strokeWidth={2}
              >
                <path strokeLinecap="round" strokeLinejoin="round" d="M6 18L18 6M6 6l12 12" />
              </svg>
            </button>
            <PropertyDetail id={selectedId} />
          </div>
        </div>
      )}
      
      <form className="bg-white p-4 rounded-xl shadow-md space-y-4">
        <div className="flex justify-center gap-4">
          <button
            type="button"
            disabled={filters.page === 1 || loading}
            onClick={prevPage}
            className="px-4 py-2 bg-gray-200 hover:bg-gray-300 rounded disabled:opacity-50"
          >
            Anterior
          </button>
          <span className="px-4 py-2 text-gray-700">
            Página {filters.page} de {totalPages}
          </span>
          <button
            type="button"
            disabled={filters.page === totalPages || loading}
            onClick={nextPage}
            className="px-4 py-2 bg-gray-200 hover:bg-gray-300 rounded disabled:opacity-50"
          >
            Siguiente
          </button>
        </div>
      </form>
    </main>
  );
}