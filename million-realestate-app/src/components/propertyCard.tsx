import Link from 'next/link';

type Property = {
  idOwner: string;
  name: string;
  address: string;
  price: number;
  imageUrl: string;
};

export default function PropertyCard({ property }: { property: Property }) {
  return (
    <Link href={`/properties/${property.idOwner}`} className="block">
        <div className="rounded-2xl border bg-white shadow-sm overflow-hidden hover:shadow-md transition">
        <img src={property.imageUrl} alt={property.name} className="w-full h-48 object-cover" />
        <div className="p-4 space-y-2">
            <h2 className="text-xl font-semibold text-gray-800">{property.name}</h2>
            <p className="text-gray-600">{property.address}</p>
            <p className="text-green-700 font-bold text-lg">${property.price.toLocaleString()}</p>
        </div>
        </div>
    </Link>
  );
}