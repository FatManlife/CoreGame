import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import axiosClient from "../../axiosClient";
import { Cookies } from "react-cookie";

export default function Login({ onLogin }) {
  const navigate = useNavigate();
  const [error, setError] = useState(null);

  const login = (username, passowrd) => {
    return axios
      .post("/user/login")
      .login(username, passowrd)
      .then((res) => {
        if (res.success) {
          Cookies.set("authToken", response.token, { expires: 7 });
        }
        return true;
      });
    return false;
  };

  const handleInput = (e) => {
    setInputData((prevInputData) => ({
      ...prevInputData,
      [e.target.name]: e.target.value,
    }));
  };

  const handleSubmit = (e) => {};

  return (
    <section>
      <section>
        {error && <h1>"{error}"</h1>}
        <form onSubmit={handleSubmit} noValidate>
          <div className="mt-4">
            <label htmlFor="email">Email</label>
            <input
              type="email"
              id="email"
              name="email"
              onChange={handleInput}
            />
          </div>
          <div className="mt-4">
            <label htmlFor="password">Password</label>
            <input
              type="password"
              id="password"
              name="hashed_password"
              onChange={handleInput}
            />
          </div>
          <div className="mt-10">
            <button type="submit"> Login</button>
          </div>
        </form>
      </section>
    </section>
  );
}
