
import React from 'react';
import { Link } from 'react-router-dom';

const NavigationBar: React.FC = () => {
  return (
    <nav>
      <ul>
        <li>
          <Link to="/">Home</Link>
        </li>
        <li>
          <Link to="/privacy-policy">Privacy Policy</Link>
        </li>
        <li>
          <Link to="/terms-of-service">Terms of Service</Link>
        </li>
        <li>
          <Link to="/data-deletion">Data Deletion</Link>
        </li>
      </ul>
    </nav>
  );
};

export default NavigationBar;
