import { useEffect, useState } from 'react';
import api from './api';

interface Task {
  id: string;
  title: string;
  description: string;
  completed: boolean;
}

export default function TaskList() {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [editingTask, setEditingTask] = useState<Task | null>(null);
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [completed, setCompleted] = useState(false);

  useEffect(() => {
    fetchTasks();
  }, []);

  const fetchTasks = async () => {
    try {
      const response = await api.getTasks();
      setTasks(response.data);
    } catch (error) {
      console.error('Failed to fetch tasks:', error);
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      if (editingTask) {
        await api.updateTask(editingTask.id, { title, description, completed });
      } else {
        await api.createTask({ title, description });
      }
      setTitle('');
      setDescription('');
      setCompleted(false);
      setEditingTask(null);
      fetchTasks();
    } catch (error) {
      console.error('Operation failed:', error);
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await api.deleteTask(id);
      fetchTasks();
    } catch (error) {
      console.error('Delete failed:', error);
    }
  };

  return (
    <div>
      <div className="container text-center">
        <div className="row">
          <div className="col align-self-center">
            <form onSubmit={handleSubmit}>
              <div className="mb-3 col align-self-center">
                <label className="form-label">Title</label>
                <input
                  className="form-control"
                  value={title}
                  onChange={(e) => setTitle(e.target.value)}
                  placeholder="Title"
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Description</label>
                <textarea
                  className="form-control"
                  value={description}
                  onChange={(e) => setDescription(e.target.value)}
                  placeholder="Description"
                />
              </div>
              <div className="mb-3">
                <input
                  role="switch"
                  id="flexSwitchCheckDefault"
                  className="me-3 bg-info form-check-input"
                  type="checkbox"
                  checked={completed}
                  onChange={(e) => setCompleted(e.target.checked)}
                />
                <label htmlFor="flexSwitchCheckDefault" className="form-check-label">
                  Completed
                </label>
              </div>
              <button className="btn btn-primary" type="submit">
                  {editingTask ? 'Update Task' : 'Add Task'}
              </button>
            </form>
          </div>
        </div>
        <div>
        <div className="mt-5 container text-center">
          <div className="row row-cols-2 row-cols-lg-5 g-2 g-lg-3">
          {tasks.length === 0 ? (
            <p>Agrega Tu primer tarea</p>
          ) : (
            tasks.map((task) => (
              <div key={task.id} 
              className={`me-3 col bg-${task.completed ? 'success' : 'warning'} border border-2 border-${task.completed ? 'success' : 'warning'} rounded-4 p-1 custom-border}`}>
                <div className="p-3">
                  <div key={task.id}>
                    <h3 className={`${task.completed ? 'text-white' : ''} h5 text-truncate`}>{task.title}</h3>
                    <p className={`${task.completed ? 'text-white' : ''} overflow-y-auto`}>{task.description}</p>
                    <div className="d-flex gap-2 flex-wrap">
                      <button 
                        type="button"
                        className="btn btn-dark flex-grow-1"
                        onClick={() => {
                          setEditingTask(task);
                          setTitle(task.title);
                          setDescription(task.description);
                          setCompleted(task.completed);
                        }}
                      >
                        Edit
                      </button>
                      <button 
                        type="button" 
                        className="btn btn-danger flex-grow-1"
                        onClick={() => handleDelete(task.id)}
                      >
                        Delete
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            ))
          )}
          </div>
          </div>
        </div>
      </div>
    </div>
  );
}