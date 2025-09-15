import { render, screen, waitFor } from "@testing-library/react";
import PropertyDetail from "@/components/propertyDetail";
import { Property } from "@/models/property";
import { PropertyTrace } from "@/models/propertyTrace";

// 🔹 Mock del módulo que trae las transacciones
jest.mock("@/lib/api", () => ({
  getPropertyTraces: jest.fn(),
}));

const mockGetPropertyTraces = require("@/lib/api").getPropertyTraces as jest.Mock;

// 🔹 Mock global de fetch
global.fetch = jest.fn();

describe("PropertyDetail", () => {
  const fakeProperty: Property = {
    id: "123",
    idOwner: "owner1",
    name: "Casa Bonita",
    address: "Calle Falsa 123",
    price: 250000,
    imageUrl: "house.jpg",
    owner: {
      id: "owner1",
      name: "Juan Pérez",
      address: "Otra calle 456",
      birthday: "1990-01-01",
      photoUrl: "owner.jpg",
    },
  };

  const fakeTraces: PropertyTrace[] = [
    {
      id: "t1",
      idProperty: "123",
      name: "Venta inicial",
      dateSale: "2022-07-10T00:00:00.000Z",
      value: 420000,
      tax: 42000,
    },
  ];

  beforeEach(() => {
    jest.clearAllMocks();

    // Mock fetch -> devuelve la propiedad
    (global.fetch as jest.Mock).mockResolvedValue({
      ok: true,
      json: async () => fakeProperty,
    });

    // Mock getPropertyTraces -> devuelve historial
    mockGetPropertyTraces.mockResolvedValue(fakeTraces);
  });

  it("muestra los detalles de la propiedad", async () => {
    render(<PropertyDetail id="123" />);

    // Esperamos a que cargue el nombre de la propiedad
    expect(await screen.findByText("Casa Bonita")).toBeInTheDocument();

    expect(screen.getByText("Calle Falsa 123")).toBeInTheDocument();
    expect(screen.getByText(/\$250,000/)).toBeInTheDocument();
    expect(screen.getByText("Juan Pérez")).toBeInTheDocument();
  });

  it("muestra historial de transacciones", async () => {
    render(<PropertyDetail id="123" />);

    expect(await screen.findByText("Venta inicial")).toBeInTheDocument();
    expect(screen.getByText(/420,000/)).toBeInTheDocument();
    expect(screen.getByText(/Impuesto/)).toBeInTheDocument();
  });

  it("muestra mensaje si no encuentra propiedad", async () => {
    // Sobrescribimos fetch para devolver error
    (global.fetch as jest.Mock).mockResolvedValueOnce({ ok: false });

    render(<PropertyDetail id="not-found" />);

    expect(
      await screen.findByText("No se encontró la propiedad.")
    ).toBeInTheDocument();
  });

  it("muestra mensaje si no hay transacciones", async () => {
    mockGetPropertyTraces.mockResolvedValueOnce([]); // sin historial

    render(<PropertyDetail id="123" />);

    expect(
      await screen.findByText("No hay transacciones registradas.")
    ).toBeInTheDocument();
  });
});
