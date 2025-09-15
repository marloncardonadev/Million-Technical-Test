'use client';

import { useState } from 'react';

type Props = {
  onSearch: (filters: any) => void;
  onSortChange: (sortBy: string, sortDirection: 'asc' | 'desc') => void;
};

export default function PropertyFilters({ onSearch, onSortChange  }: Props) {
  const [name, setName] = useState('');
  const [address, setAddress] = useState('');
  const [minPrice, setMinPrice] = useState('');
  const [maxPrice, setMaxPrice] = useState('');
  const [sort, setSort] = useState('Price-asc');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSearch({
      name,
      address,
      minPrice: minPrice ? Number(minPrice) : undefined,
      maxPrice: maxPrice ? Number(maxPrice) : undefined,
      page: 1,
      limit: 25
    });
  };

  const handleSortChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setSort(e.target.value);
    const [sortBy, sortDirectionRaw] = e.target.value.split('-');
    onSortChange(sortBy, sortDirectionRaw as 'asc' | 'desc');
  };

  return (
    <form
      onSubmit={handleSubmit}
      className="bg-white p-4 rounded-xl shadow-md space-y-4"
    >
      <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
        <input
          type="text"
          placeholder="Nombre"
          value={name}
          onChange={(e) => setName(e.target.value)}
          className="rounded-lg px-3 py-2 text-sm shadow-md focus:outline-none focus:ring-1 focus:ring-gray-400"
        />
        <input
          type="text"
          placeholder="Dirección"
          value={address}
          onChange={(e) => setAddress(e.target.value)}
          className="rounded-lg px-3 py-2 text-sm shadow-md focus:outline-none focus:ring-2 focus:ring-gray-400"
        />
        <input
          type="number"
          placeholder="Precio Mínimo"
          value={minPrice}
          onChange={(e) => setMinPrice(e.target.value)}
          className="rounded-lg px-3 py-2 text-sm shadow-md focus:outline-none focus:ring-2 focus:ring-gray-400"
        />
        <input
          type="number"
          placeholder="Precio Máximo"
          value={maxPrice}
          onChange={(e) => setMaxPrice(e.target.value)}
          className="rounded-lg px-3 py-2 text-sm shadow-md focus:outline-none focus:ring-2 focus:ring-gray-400"
        />
      </div>

      <div className="flex flex-col md:flex-row md:items-center md:justify-between gap-4">
        <button
          type="submit"
          className="px-4 py-2 bg-gray-200 hover:bg-gray-300 rounded disabled:opacity-50"
        >
          Buscar
        </button>

        <div className="flex items-center gap-2">
          <label htmlFor="sort" className="text-sm text-gray-600">Ordenar por:</label>
          <select
            id="sort"
            value={sort}
            onChange={handleSortChange}
            className="rounded-lg px-3 py-2 text-sm shadow-md focus:outline-none focus:ring-1 focus:ring-gray-400"
          >
            <option value="Price-asc">Precio ↑</option>
            <option value="Price-desc">Precio ↓</option>
            <option value="Name-asc">Nombre A-Z</option>
            <option value="Name-desc">Nombre Z-A</option>
          </select>
        </div>
      </div>
    </form>
  );
}