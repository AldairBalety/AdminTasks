import React from 'react';
import { Link } from 'react-router-dom';

const Sidebar: React.FC = () => {
  return (
    <aside>
      <nav>
        <ul>
          <li><Link to="/">Dashboard</Link></li>
          <li><Link to="/news">News</Link></li>
          <li><Link to="/settings">Settings</Link></li>
          <hr />
          <li><Link to="/privacy-policy">Privacy Policy</Link></li>
          <li><Link to="/terms-of-service">Terms of Service</Link></li>
          <li><Link to="/data-deletion">Data Deletion</Link></li>
        </ul>
      </nav>
    </aside>
  );
};

export default Sidebar;
