'use client';

import { useState } from 'react';

type Props = {
  onSearch: (filters: any) => void;
};

export default function PropertyFilters({ onSearch }: Props) {
  const [name, setName] = useState('');
  const [address, setAddress] = useState('');
  const [minPrice, setMinPrice] = useState('');
  const [maxPrice, setMaxPrice] = useState('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSearch({
      name,
      address,
      minPrice: minPrice ? Number(minPrice) : undefined,
      maxPrice: maxPrice ? Number(maxPrice) : undefined,
      page: 1,
      limit: 10
    });
  };

  return (
    <form onSubmit={handleSubmit} className="grid grid-cols-1 md:grid-cols-4 gap-4 w-full">
  <input type="text" placeholder="Nombre" value={name}
    onChange={(e) => setName(e.target.value)}
    className="border rounded-xl px-3 py-2 text-sm" />
  <input type="text" placeholder="Dirección" value={address}
    onChange={(e) => setAddress(e.target.value)}
    className="border rounded-xl px-3 py-2 text-sm" />
  <input type="number" placeholder="Precio Mínimo" value={minPrice}
    onChange={(e) => setMinPrice(e.target.value)}
    className="border rounded-xl px-3 py-2 text-sm" />
  <input type="number" placeholder="Precio Máximo" value={maxPrice}
    onChange={(e) => setMaxPrice(e.target.value)}
    className="border rounded-xl px-3 py-2 text-sm" />
  <div className="md:col-span-4">
    <button type="submit"
      className="w-full bg-blue-600 text-white py-2 rounded-xl hover:bg-blue-700 transition">
      Buscar
    </button>
  </div>
</form>
  );
}