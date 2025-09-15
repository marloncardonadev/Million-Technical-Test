import { Property } from "@/models/property";


export default function PropertyCard({
  property,
  onClick,
}: {
  property: Property;
  onClick: () => void;
}) {
  return (
    <div
      data-testid="property-card"
      onClick={onClick}
      className="cursor-pointer rounded-2xl bg-white shadow-md hover:shadow-lg transition flex flex-col"
    >
      <img
        src={property.imageUrl}
        alt={property.name}
        className="w-full h-48 object-cover"
      />
      <div className="p-4 flex flex-col justify-between flex-1">
        <div className="space-y-2">
          <h2 className="text-xl font-semibold text-gray-800">
            {property.name}
          </h2>
          <p className="text-gray-600">{property.address}</p>
        </div>
        <p className="text-green-700 font-bold text-lg mt-4">
          ${property.price.toLocaleString()}
        </p>
      </div>
    </div>
  );
}