import { useEffect, useState } from 'react';
import api from './api';
import { useAuth } from './AuthContext';

export default function Profile() {
  const { user, logout } = useAuth();
  const [name, setName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const response = await api.getUser();
        setName(response.data.name);
        setLastName(response.data.lastName);
        setEmail(response.data.email);
        setPassword(response.data.password);
      } catch (error) {
        console.error('Failed to fetch user:', error);
      }
    };
    fetchUser();
  }, []);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await api.updateUser({ name, email });
    } catch (error) {
      console.error('Update failed:', error);
    }
  };

  return (
    <div className="container text-center">
      <div className="row">
        <div className="col align-self-center">
          <form onSubmit={handleSubmit}>
            <div className="mb-3 col align-self-center">
              <label className="form-label">Name</label>
              <input
                className="form-control"
                type="name"
                value={name}
                onChange={(e) => setName(e.target.value)}
              />
            </div>
            <div className="mb-3">
              <label className="form-label">LastNae</label>
              <input
                className="form-control"
                type="lastName"
                value={lastName}
                onChange={(e) => setLastName(e.target.value)}
              />
            </div>
            <div className="mb-3">
              <label className="form-label">Email</label>
              <input
                className="form-control"
                type="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
            </div>
            <button className="btn btn-primary" type="submit" onClick={logout} >Logout</button>
          </form>
        </div>
      </div>
    </div>
  );
}