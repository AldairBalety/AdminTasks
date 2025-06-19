import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Header from './components/Header';
import Sidebar from './components/Sidebar';
import Footer from './components/Footer';

// Pages
import Dashboard from './pages/Dashboard';
import News from './pages/News';
import Settings from './pages/Settings';
import PrivacyPolicy from './pages/PrivacyPolicy';
import TermsOfService from './pages/TermsOfService';
import DataDeletion from './pages/DataDeletion';

// Styles
import './styles/Admin.css';

function App() {
  return (
    <Router>
      <div className="app">
        <Sidebar />
        <main className="main-content">
          <Header />
          <Routes>
            <Route path="/" element={<Dashboard />} />
            <Route path="/news" element={<News />} />
            <Route path="/settings" element={<Settings />} />
            <Route path="/privacy-policy" element={<PrivacyPolicy />} />
            <Route path="/terms-of-service" element={<TermsOfService />} />
            <Route path="/data-deletion" element={<DataDeletion />} />
          </Routes>
          <Footer />
        </main>
      </div>
    </Router>
  );
}

export default App;