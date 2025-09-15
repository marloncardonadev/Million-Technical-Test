import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import PropertyCard from '@/components/propertyCard';

// Mock para las imágenes
jest.mock('next/image', () => ({
  __esModule: true,
  default: (props: any) => {
    return <img {...props} />;
  },
}));

const mockProperty = {
  id: '1',
  idOwner: "owner1",
  name: 'Beautiful House',
  address: '123 Main St',
  price: 250000,
  imageUrl: '/test-image.jpg'
};

describe('PropertyCard', () => {
  it('renders property information correctly', () => {
    const mockOnClick = jest.fn();
    
    render(<PropertyCard property={mockProperty} onClick={mockOnClick} />);
    
    expect(screen.getByText('Beautiful House')).toBeInTheDocument();
    expect(screen.getByText('123 Main St')).toBeInTheDocument();
    expect(screen.getByText('$250,000')).toBeInTheDocument();
    expect(screen.getByAltText('Beautiful House')).toHaveAttribute('src', '/test-image.jpg');
  });

  it('calls onClick when clicked', async () => {
    const user = userEvent.setup();
    const mockOnClick = jest.fn();
    
    render(<PropertyCard property={mockProperty} onClick={mockOnClick} />);
    
    await user.click(screen.getByTestId('property-card'));
    
    expect(mockOnClick).toHaveBeenCalledTimes(1);
  });
});