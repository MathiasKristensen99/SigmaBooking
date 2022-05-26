import { ClientFunction } from "testcafe";

fixture`Navbar tests`;

test.page("https://sigmabooking-68a9f.web.app/")("GoToHomeView", async (t) => {
  await t.click("#app > header > div > nav > div > button > span");
  await t.click("#navbarToggler > ul > li:nth-child(1) > a");
  const location = await ClientFunction(() => document.location.href);
  await t.expect(location()).contains("/home");
});
test.page("https://sigmabooking-68a9f.web.app/")(
  "GoToReservationView",
  async (t) => {
    await t.click("#app > header > div > nav > div > button > span");
    await t.click("#navbarToggler > ul > li:nth-child(2) > a");
    const location = await ClientFunction(() => document.location.href);
    await t.expect(location()).contains("/reservation");
  }
);
