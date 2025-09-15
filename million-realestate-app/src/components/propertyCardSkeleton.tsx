export default function PropertyCardSkeleton() {
  return (
    <div className="h-full flex flex-col rounded-2xl bg-white shadow-sm overflow-hidden animate-pulse">
      <div className="w-full h-48 bg-gray-200" />
      <div className="p-4 space-y-2 flex-1 flex flex-col justify-between">
        <div>
          <div className="h-6 bg-gray-200 rounded w-3/4 mb-2" />
          <div className="h-4 bg-gray-200 rounded w-1/2" />
        </div>
        <div className="h-5 bg-gray-200 rounded w-1/3" />
      </div>
    </div>
  );
}