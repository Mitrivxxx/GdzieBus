import { test, expect } from '@playwright/test';

test('React → Backend → DB integration', async ({ page }) => {
  await page.goto('http://localhost:3000');

  const status = await page.locator('#health-status').innerText();
  expect(status).toContain('ok');
});
