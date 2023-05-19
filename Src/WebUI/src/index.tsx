import ReactDOM from 'react-dom/client';
import reportWebVitals from './reportWebVitals';
import './app/layout/styles.css';
import { RouterProvider } from 'react-router-dom';
import { router } from './app/router/Routes';
import { StoreContext, store } from './app/stores/store';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <StoreContext.Provider value={store}>
      <RouterProvider router={router} />
  </StoreContext.Provider>
);

reportWebVitals();
