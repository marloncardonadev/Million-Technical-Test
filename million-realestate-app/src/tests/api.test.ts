import { getProperties, getPropertyTraces } from "@/lib/api";
import { PropertyTrace } from "@/models/propertyTrace";

// 👇 reseteamos mocks antes de cada test
beforeEach(() => {
  jest.resetAllMocks();
});

describe("api.ts", () => {
  it("getProperties devuelve datos cuando fetch es exitoso", async () => {
    const fakeResponse = { items: [{ id: "1", name: "Casa bonita" }], totalPages: 2 };

    global.fetch = jest.fn().mockResolvedValue({
      ok: true,
      json: async () => fakeResponse,
    } as Response);

    const filters = { page: 1, limit: 10 };
    const result = await getProperties(filters);

    expect(fetch).toHaveBeenCalledWith(
      expect.stringContaining("api/property/getfiltered?page=1&limit=10"),
      expect.objectContaining({ cache: "no-store" })
    );
    expect(result).toEqual(fakeResponse);
  });

  it("getProperties lanza error si fetch falla", async () => {
    global.fetch = jest.fn().mockResolvedValue({
      ok: false,
    } as Response);

    await expect(getProperties({})).rejects.toThrow("Error al obtener propiedades");
  });

  it("getPropertyTraces devuelve lista de transacciones cuando fetch es exitoso", async () => {
    const fakeTraces: PropertyTrace[] = [
      {
        id: "t1",
        idProperty: "p1",
        name: "Venta inicial",
        dateSale: new Date().toISOString(),
        value: 100000,
        tax: 10000,
      },
    ];

    global.fetch = jest.fn().mockResolvedValue({
      ok: true,
      json: async () => fakeTraces,
    } as Response);

    const result = await getPropertyTraces("p1");

    expect(fetch).toHaveBeenCalledWith(
      "https://localhost:7177/api/propertytrace/byProperty/p1",
      expect.objectContaining({ cache: "no-store" })
    );
    expect(result).toEqual(fakeTraces);
  });

  it("getPropertyTraces devuelve [] si fetch falla", async () => {
    global.fetch = jest.fn().mockResolvedValue({
      ok: false,
    } as Response);

    const result = await getPropertyTraces("p1");
    expect(result).toEqual([]);
  });
});
