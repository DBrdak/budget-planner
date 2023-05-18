import HomePage from '../../features/home/HomePage';
import { observer } from 'mobx-react-lite';
import { Outlet, useLocation } from 'react-router-dom';
import MainPage from '../../features/main/dashboard/MainPage';

function App() {

  return (
    <>
      <Outlet />
    </>
);
}

export default observer(App);
