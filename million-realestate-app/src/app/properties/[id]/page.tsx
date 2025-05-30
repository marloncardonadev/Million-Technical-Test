import { notFound } from 'next/navigation';

type Property = {
  idOwner: string;
  name: string;
  address: string;
  price: number;
  imageUrl: string;
};

async function getPropertyById(id: string): Promise<Property | null> {
  const res = await fetch(`https://localhost:7177/api/properties/${id}`, {
    cache: 'no-store'
  });

  if (!res.ok) return null;
  return res.json();
}

export default async function PropertyDetailPage({ params }: { params: { id: string } }) {
  const property = await getPropertyById(params.id);

  if (!property) {
    return notFound();
  }

  return (
    <main className="max-w-4xl mx-auto p-6">
      <img src={property.imageUrl} alt={property.name} className="w-full h-72 object-cover rounded-xl shadow mb-6" />
      <h1 className="text-3xl font-bold mb-2">{property.name}</h1>
      <p className="text-gray-600 mb-4">{property.address}</p>
      <p className="text-green-700 text-2xl font-semibold mb-6">${property.price.toLocaleString()}</p>

      <div className="text-sm text-gray-500">Propietario: {property.idOwner}</div>
    </main>
  );
}