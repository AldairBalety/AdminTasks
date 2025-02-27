import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:4000/api',
});

api.interceptors.request.use((config) => {
  const user = JSON.parse(localStorage.getItem('user') || 'null');
  if (user?.token) {
    config.headers.Authorization = `Bearer ${user.token}`;
  }
  return config;
});

export default {
  // User endpoints
  login: (data: { username: string; password: string }) =>
    api.post('/user/login', data),
  register: (data: { name: string; lastName: string, email: string; password: string }) =>
    api.post('/user', data),
  getUser: () => api.get('/user'),
  updateUser: (data: { name?: string; lastName?: string, email?: string }) =>
    api.put('/user', data),

  // Task endpoints
  getTasks: () => api.get('/tasks'),
  createTask: (data: { title: string; description: string }) =>
    api.post('/tasks', data),
  updateTask: (id: string, data: { title?: string; description?: string, completed: boolean }) =>
    api.put(`/tasks/${id}`, data),
  deleteTask: (id: string) => api.delete(`/tasks/${id}`),
};