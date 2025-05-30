export type PropertyFilter = {
  name?: string;
  address?: string;
  minPrice?: number;
  maxPrice?: number;
  page?: number;
  limit?: number;
  sortBy?: string;
  sortDirection?: 'asc' | 'desc';
};

export const getProperties = async (filters: PropertyFilter = {}) => {
  const query = new URLSearchParams(
    Object.entries(filters).reduce((acc, [key, val]) => {
      if (val !== undefined && val !== "") acc[key] = String(val);
      return acc;
    }, {} as Record<string, string>)
  );

  const res = await fetch(`https://localhost:7177/api/properties/getfiltered?${query}`, {
    cache: 'no-store'
  });

  if (!res.ok) throw new Error('Error al obtener propiedades');
  return res.json(); // espera estructura paginada
};