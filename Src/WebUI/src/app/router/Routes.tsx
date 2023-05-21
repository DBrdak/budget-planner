import { RouteObject } from "react-router";
import { Navigate, createBrowserRouter } from "react-router-dom"
import MainPage from "../../features/main/dashboard/MainPage";
import GoalsPage from "../../features/goals/GoalsPage";
import App from "../layout/App";
import HomePage from "../../features/home/HomePage";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            {path: 'main', element: <MainPage/>},
            {path: 'goals', element: <GoalsPage/>},
            {path: '*', element: <Navigate replace to='/' />}
        ]
    }
]

export const router = createBrowserRouter(routes);