import HomePage from '../../features/home/HomePage';
import { observer } from 'mobx-react-lite';
import { Outlet, useLocation } from 'react-router-dom';
import ModalContainer from '../common/modals/ModalContainer';
import './styles.css';
import { ToastContainer } from 'react-toastify';

function App() {
  const location = useLocation();

  return (
    <>
      <ModalContainer />
      <ToastContainer position='bottom-right' hideProgressBar theme='colored'/>
      {location.pathname === '/' ? <HomePage /> :(
        <>
        <Outlet />
        </>)}
    </>
);
}

export default observer(App);
