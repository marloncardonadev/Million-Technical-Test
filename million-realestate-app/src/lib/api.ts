import { PropertyFilter } from "@/models/propertyFilter";
import { PropertyTrace } from "@/models/propertyTrace";

export const getProperties = async (filters: PropertyFilter = {}) => {
  const query = new URLSearchParams(
    Object.entries(filters).reduce((acc, [key, val]) => {
      if (val !== undefined && val !== "") acc[key] = String(val);
      return acc;
    }, {} as Record<string, string>)
  );

  const res = await fetch(`https://localhost:7177/api/property/getfiltered?${query}`, {
    cache: 'no-store'
  });

  if (!res.ok) throw new Error('Error al obtener propiedades');
  return res.json();
};

export async function getPropertyTraces(propertyId: string): Promise<PropertyTrace[]> {
  const res = await fetch(`https://localhost:7177/api/propertytrace/byProperty/${propertyId}`, {
    cache: "no-store",
  });

  if (!res.ok) return [];
  return res.json();
}