import { Selector, ClientFunction } from "testcafe";

fixture`Navbar tests`;

test.page("http://localhost:3000/")("GoToHomeView"),
  async (t) => {
    await t.click("#navbarToggler > ul > li:nth-child(1) > a");
    const location = await ClientFunction(() => document.location.href);
    await t.expect(location()).contains("/home");
  };

test.page("http://localhost:3000/)(GoToReservationView", async (t) => {
  await t.click("#navbarToggler > ul > li:nth-child(2) > a");
  const location = await ClientFunction(() => document.location.href);
  await t.expect(location()).contains("/reservation");
});

test.page("write admin password", async (t) => {
  await t
    .click("#app > div.row > div.col-3.btnDiv > button:nth-child(3)")
    .typeText("#password", "1234")
    .click('button[title="Log In"]');
});
