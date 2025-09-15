import '@testing-library/jest-dom';

// 👇 Opcional: si usas fetch en tu app y quieres mockearlo
import 'whatwg-fetch';

// 👇 Opcional: si usas window.matchMedia (Next.js rompe si no existe en test)
Object.defineProperty(window, 'matchMedia', {
  writable: true,
  value: (query: string) => ({
    matches: false,
    media: query,
    onchange: null,
    addListener: () => {}, // obsoleto
    removeListener: () => {}, // obsoleto
    addEventListener: () => {},
    removeEventListener: () => {},
    dispatchEvent: () => false,
  }),
});