import { useState, useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useAuth } from './AuthContext';
import styled from 'styled-components';

const styles = {
    nav: {
        display: 'flex',
        justifyContent: 'space-between',
        padding: '1rem 2rem',
        backgroundColor: '#333',
        borderBottom: '1px solid #ddd',
        boxShadow: '0 2px 5px rgba(0,0,0,0.1)',
        position: 'sticky',
        top: 0,
        zIndex: 100,
    },
    link: {
        textDecoration: 'none',
        color: '#fff',
        padding: '0.5rem 1rem',
        borderRadius: '8px',
        fontSize: '1.1rem',
        fontWeight: 600,
        transition: 'background-color 0.3s ease, color 0.3s ease',
    },
    linkHover: {
        backgroundColor: '#444',
        color: '#f0f0f0',
    },
    linkLoginRegister: {
        textDecoration: 'none',
        color: '#fff',
        padding: '0.5rem 1rem',
        borderRadius: '8px',
        fontSize: '1rem',
        fontWeight: 600,
        transition: 'background-color 0.3s ease',
    },
    linkLoginRegisterHover: {
        backgroundColor: '#555',
    },
    linkProfile: {
        display: 'block',
        padding: '0.5rem',
        textDecoration: 'none',
        color: '#333',
        borderRadius: '8px',
        transition: 'background-color 0.3s ease',
    },
    linkProfileHover: {
        backgroundColor: '#f0f0f0',
    },
    buttonCloseSession: {
        width: '100%',
        padding: '0.5rem',
        background: 'none',
        border: 'none',
        cursor: 'pointer',
        textAlign: 'left',
        color: '#333',
        fontSize: '1rem',
        transition: 'background-color 0.3s ease',
        borderRadius: '8px',
    },
    buttonCloseSessionHover: {
        backgroundColor: '#f7f7f7',
    },
    buttonUser: {
        background: 'none',
        border: 'none',
        cursor: 'pointer',
        display: 'flex',
        alignItems: 'center',
        gap: '0.5rem',
        fontSize: '1rem',
        color: '#fff',
    },
    divProfile: {
        position: 'absolute',
        right: 0,
        top: '100%',
        backgroundColor: 'white',
        border: '1px solid #ddd',
        borderRadius: '8px',
        padding: '0.5rem',
        minWidth: '180px',
        boxShadow: '0 4px 10px rgba(0,0,0,0.1)',
        zIndex: 200,
    },
    divLoginResgister: {
        display: 'flex', 
        gap: '1.5rem',
    },
    divPosition: {
        position: 'relative',
    },
  } as const;
  
  const NavbarLink = styled(Link)`
    display: flex;
    text-decoration: none;
    color: #fff;
    padding: 0.5rem 1rem;
    border-radius: 8px;
    font-size: 1.1rem;
    font-weight: 600;
    transition: background-color 0.3s ease, color 0.3s ease;
  
    &:hover {
      background-color: #444;
      color: #f0f0f0;
    }
  `;
  
  const NavbarLinkLoginRegister = styled(Link)`
    text-decoration: none;
    color: #fff;
    padding: 0.5rem 1rem;
    border-radius: 8px;
    font-size: 1rem;
    font-weight: 600;
    transition: background-color 0.3s ease;
  
    &:hover {
      background-color: #555;
    }
  `;
  
  export default function Navbar() {
    const { user, logout } = useAuth();
    const [isDropdownOpen, setIsDropdownOpen] = useState(false);
    const navigate = useNavigate();
  
    const handleLogout = () => {
      logout();
      navigate('/login');
      setIsDropdownOpen(false);
    };
  
    useEffect(() => {
      const handleClickOutside = (e: MouseEvent) => {
        if (!(e.target as Element).closest('.dropdown-container')) {
          setIsDropdownOpen(false);
        }
      };
  
      document.addEventListener('click', handleClickOutside);
      return () => document.removeEventListener('click', handleClickOutside);
    }, []);
  
    return (
      <nav style={styles.nav}>
        <div className="dropdown-container">
          <NavbarLink to="/">Tareas</NavbarLink>
        </div>
        <div style={styles.divPosition} className="dropdown-container">
          {user ? (
            <div>
              <button 
                onClick={() => setIsDropdownOpen(!isDropdownOpen)}
                style={styles.buttonUser}
              >
                <span>👤</span>
                <span>{user.name}</span>
                <span>▼</span>
              </button>
              
              {isDropdownOpen && (
                <div style={styles.divProfile}>
                  <Link
                    to="/profile"
                    onClick={() => setIsDropdownOpen(false)}
                    style={styles.linkProfile}
                  >
                    Perfil
                  </Link>
                  <button
                    onClick={handleLogout}
                    style={styles.buttonCloseSession}
                  >
                    Cerrar sesión
                  </button>
                </div>
              )}
            </div>
          ) : (
            <div style={styles.divLoginResgister}>
              <NavbarLinkLoginRegister to="/login">Login</NavbarLinkLoginRegister>
              <NavbarLinkLoginRegister to="/register">Registro</NavbarLinkLoginRegister>
            </div>
          )}
        </div>
      </nav>
    );
  }
  