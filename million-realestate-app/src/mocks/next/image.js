import React from 'react';

const Image = ({ src, alt, width, height, ...props }) => {
  return React.createElement('img', {
    src: src,
    alt: alt,
    width: width,
    height: height,
    ...props,
  });
};

export default Image;