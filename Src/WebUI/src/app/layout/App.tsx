import HomePage from '../../features/home/HomePage';
import { observer } from 'mobx-react-lite';
import { Outlet, useLocation } from 'react-router-dom';
import MainPage from '../../features/main/dashboard/MainPage';
import ModalContainer from '../common/modals/ModalContainer';

function App() {
  const location = useLocation();

  return (
    <>
      <ModalContainer />
      {location.pathname === '/' ? <HomePage /> :(
        <>
        <Outlet />
        </>)}
    </>
);
}

export default observer(App);
