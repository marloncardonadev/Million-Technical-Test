import { render, screen, fireEvent } from "@testing-library/react";
import PropertyFilters from "@/components/propertyFilters";

describe("PropertyFilters", () => {
  it("renderiza correctamente los campos y botones", () => {
    render(
      <PropertyFilters onSearch={jest.fn()} onSortChange={jest.fn()} />
    );

    expect(screen.getByPlaceholderText("Nombre")).toBeInTheDocument();
    expect(screen.getByPlaceholderText("Dirección")).toBeInTheDocument();
    expect(screen.getByPlaceholderText("Precio Mínimo")).toBeInTheDocument();
    expect(screen.getByPlaceholderText("Precio Máximo")).toBeInTheDocument();
    expect(screen.getByText("Buscar")).toBeInTheDocument();
    expect(screen.getByLabelText("Ordenar por:")).toBeInTheDocument();
  });

  it("llama a onSearch con los filtros al enviar el formulario", () => {
    const mockOnSearch = jest.fn();

    render(
      <PropertyFilters onSearch={mockOnSearch} onSortChange={jest.fn()} />
    );

    fireEvent.change(screen.getByPlaceholderText("Nombre"), {
      target: { value: "Casa bonita" },
    });
    fireEvent.change(screen.getByPlaceholderText("Dirección"), {
      target: { value: "Calle 123" },
    });
    fireEvent.change(screen.getByPlaceholderText("Precio Mínimo"), {
      target: { value: "100000" },
    });
    fireEvent.change(screen.getByPlaceholderText("Precio Máximo"), {
      target: { value: "200000" },
    });

    const form = screen.getByText("Buscar").closest("form");
    fireEvent.submit(form!);

    expect(mockOnSearch).toHaveBeenCalledWith({
      name: "Casa bonita",
      address: "Calle 123",
      minPrice: 100000,
      maxPrice: 200000,
      page: 1,
      limit: 25,
    });
  });

  it("llama a onSortChange cuando cambia el select de orden", () => {
    const mockOnSortChange = jest.fn();

    render(
      <PropertyFilters onSearch={jest.fn()} onSortChange={mockOnSortChange} />
    );

    fireEvent.change(screen.getByLabelText("Ordenar por:"), {
      target: { value: "Name-desc" },
    });

    expect(mockOnSortChange).toHaveBeenCalledWith("Name", "desc");
  });
});
