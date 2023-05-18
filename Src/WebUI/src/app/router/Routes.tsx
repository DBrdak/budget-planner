import { RouteObject } from "react-router";
import { createBrowserRouter } from "react-router-dom"
import MainPage from "../../features/main/dashboard/MainPage";
import GoalsPage from "../../features/goals/GoalsPage";
import App from "../layout/App";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            {path: 'main', element: <MainPage/>},
            {path: 'goals', element: <GoalsPage/>},
        ]
    }
]

export const router = createBrowserRouter(routes);