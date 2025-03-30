import { useState, useRef } from "react";
import axiosClient from "../../axiosClient";
import { validEmail, validUsername } from "../../Validations/validUser";

export default function Sigin() {
  const [data, setData] = useState({
    username: "",
    email: "",
    hashed_password: "",
    date_created: "",
  });
  const [error, setError] = useState();
  const password2Ref = useRef(null);

  const handleInput = (e) => {
    setData((prevData) => ({
      ...prevData,
      [e.target.name]: e.target.value,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    setError(null);

    if (!validUsername.test(data.username)) {
      setError("Username is invalid");
      return;
    }

    if (!validEmail.test(data.email)) {
      setError("Emaill is invalid");
      return;
    }

    if (data.hashed_password.length < 6) {
      setError("Password is too short");
      return;
    }

    if (data.hashed_password != password2Ref.current.value) {
      setError("Passwords dont match");
      return;
    }

    const updatedData = {
      ...data,
      date_created: new Date().toISOString(),
    };

    axiosClient
      .post("user", updatedData)
      .then((response) => console.log(response))
      .catch((error) => console.log(error));
  };

  return (
    <section>
      {error && <h1>"{error}"</h1>}
      <form onSubmit={handleSubmit} noValidate>
        <div className="mt-4">
          <label htmlFor="username">Username</label>
          <input
            type="text"
            id="username"
            name="username"
            onChange={handleInput}
          />
        </div>
        <div className="mt-4">
          <label htmlFor="email">Email</label>
          <input type="email" id="email" name="email" onChange={handleInput} />
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
        <div className="mt-4">
          <label htmlFor="password2">Confim Password</label>
          <input
            type="password"
            id="password2"
            name="password2"
            ref={password2Ref}
          />
        </div>
        <div className="mt-10">
          <button type="submit"> SignIn</button>
        </div>
      </form>
    </section>
  );
}
