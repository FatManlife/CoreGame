import { Route, Routes, BrowserRouter } from "react-router";
import Layout from "./Layouts/Layout";
import Sigin from "./pages/Auth/Sigin";
import Login from "./pages/Auth/Login";
import ShowUsers from "./pages/Auth/ShowUsers";

export default function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<Layout />}>
          <Route path="/" element={<ShowUsers />} />
          <Route path="/sigin" element={<Sigin />} />
          <Route path="/login" element={<Login />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
