import { RouteObject } from "react-router";
import { Navigate, createBrowserRouter } from "react-router-dom"
import MainPage from "../../features/main/MainPage";
import GoalsPage from "../../features/goals/GoalsPage";
import App from "../layout/App";
import HomePage from "../../features/home/HomePage";
import SpendingPlan from "../../features/spendingPlan/SpendingPlan";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            {path: 'main', element: <MainPage/>},
            {path: 'goals', element: <GoalsPage/>},
            {path: 'spendingplan', element: <SpendingPlan />},
            {path: '*', element: <Navigate replace to='/' />}
        ]
    }
]

export const router = createBrowserRouter(routes);