// src/components/PropertyDetail.tsx
"use client";

import { useEffect, useState } from "react";
import PropertyDetailSkeleton from "@/components/propertyDetailSkeleton";
import { Property } from "@/models/property";
import { PropertyTrace } from "@/models/propertyTrace";
import { getPropertyTraces } from "@/lib/api";

async function getPropertyById(id: string): Promise<Property | null> {
  const res = await fetch(`https://localhost:7177/api/property/${id}`, {
    cache: "no-store",
  });

  if (!res.ok) return null;
  return res.json();
}

export default function PropertyDetail({ id }: { id: string }) {
  const [property, setProperty] = useState<Property | null>(null);
  const [traces, setTraces] = useState<PropertyTrace[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    Promise.all([getPropertyById(id), getPropertyTraces(id)])
      .then(([prop, trc]) => {
        setProperty(prop);
        setTraces(trc)
      })
      .finally(() => setLoading(false));
  }, [id]);

  if (loading) return <PropertyDetailSkeleton />;
  if (!property) return <p>No se encontró la propiedad.</p>;

  return (
    <div className="space-y-6">
      <div className="rounded-xl overflow-hidden shadow-lg">
        <img
          src={
            property.imageUrl?.startsWith("http") ? property.imageUrl : "/" + property.imageUrl
          }
          alt={property.name}
          className="w-full h-80 object-cover"
        />
      </div>
      <form className="bg-white p-4 rounded-xl shadow-md space-y-4">
        <section>
          <h1 className="text-3xl font-bold text-gray-900">{property.name}</h1>
          <p className="text-gray-600 text-lg">{property.address}</p>
          <p className="text-green-700 text-2xl font-semibold">
            ${property.price.toLocaleString()}
          </p>
        </section>
      </form>
      <form className="bg-white p-4 rounded-xl shadow-md space-y-4">
        <section>
          <h2 className="text-2xl font-semibold text-gray-800 mb-2">Descripción</h2>
          <p className="text-gray-600 leading-relaxed">
            Esta propiedad es ideal para familias que buscan comodidad y una excelente ubicación.
          </p>
        </section>
      </form>
      <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
      <form className="bg-white p-4 rounded-xl shadow-md space-y-4">
        {property.owner && (
          <section>
            <h2 className="text-2xl font-semibold text-gray-800 mb-4">Propietario</h2>
            <div className="flex items-center gap-4">
              <img src={ property.owner.photoUrl.startsWith("http") ? property.owner.photoUrl : "/" + property.owner.photoUrl }
                alt={property.owner.name}
                className="w-16 h-16 rounded-full object-cover shadow"
              />
              <div>
                <p className="text-lg font-semibold text-gray-800">
                  {property.owner.name}
                </p>
              </div>
            </div>
          </section>
        )}
      </form>
      <form className="bg-white p-4 rounded-xl shadow-md space-y-4">
        <section>
        <h2 className="text-2xl font-semibold text-gray-800 mb-4">Historial de transacciones</h2>
        {traces.length > 0 ? (
          <ul className="space-y-2">
            {traces.map((t) => (
              <li key={t.id} className="p-4 border rounded-lg shadow-sm flex justify-between">
                <div>
                  <p className="font-semibold">{t.name}</p>
                  <p className="text-sm text-gray-500">{new Date(t.dateSale).toLocaleDateString()}</p>
                </div>
                <div className="text-right">
                  <p className="text-green-700 font-bold">${t.value.toLocaleString()}</p>
                  <p className="text-sm text-gray-500">Impuesto: ${t.tax.toLocaleString()}</p>
                </div>
              </li>
            ))}
          </ul>
        ) : (
          <p className="text-gray-500">No hay transacciones registradas.</p>
        )}
      </section>
      </form>
      </div>
    </div>
  );
}
