import { RouteObject } from "react-router";
import { createBrowserRouter, Navigate } from "react-router-dom"
import MainPage from "../../features/main/dashboard/MainPage";
import GoalsPage from "../../features/goals/GoalsPage";
import App from "../layout/App";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            {path: 'MainPage', element: <MainPage/>},
            {path: 'GoalsPage', element: <GoalsPage/>},
        ]
    }
]

export const router = createBrowserRouter(routes);