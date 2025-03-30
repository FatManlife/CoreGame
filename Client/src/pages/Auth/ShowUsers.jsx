import { useState, useEffect } from "react";
import axiosClient from "../../axiosClient";

export default function ShowUsers() {
  const [data, setData] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const res = await axiosClient.get("/user");
        setData(res.data);
        console.log(res);
      } catch (err) {
        console.error(err);
      }
    };

    fetchData();
  }, []);

  return (
    <section>
      {data &&
        data.map((item) => (
          <div key={item.id}>
            {item.username}
            <br />
            {item.email}
            <br />
            {item.hashed_password}
            <br />
            {item.date_created}
            <br />
            <br />
          </div>
        ))}
    </section>
  );
}
